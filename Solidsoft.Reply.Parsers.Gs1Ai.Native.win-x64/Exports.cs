namespace Solidsoft.Reply.Parsers.Gs1Ai.Native;

using System;
using System.Runtime.InteropServices;

/// <summary>
///   Native exports for the GS1 AI parser.
/// </summary>
public static unsafe class Exports
{
    // Callback signature: identifier (UTF-16), idLen, value (UTF-16), valLen, entity, dataTitle (UTF-16), dtLen, description (UTF-16), descLen, inverseExponent, sequence, isFixedWidth, isError, isFatal
    private static delegate* unmanaged<char*, int, char*, int, int, char*, int, char*, int, int, int, int, int, int, int, int, void> Callback;

    // Exception signature: entity, errorNumber, message (UTF-16), msgLen, isFatal, offset
    private static delegate* unmanaged<int, int, char*, int, int, int, void> ExceptionCallback;

    /// <summary>
    /// Sets the main callback.
    /// </summary>
    /// <param name="cb">The callback function.</param>
    /// <returns>0 on success, -1 on failure.</returns>
    [UnmanagedCallersOnly(EntryPoint = "Gs1_SetCallback")]
    public static int Gs1_SetCallback(delegate* unmanaged<char*, int, char*, int, int, char*, int, char*, int, int, int, int, int, int, int, int, void> cb) {
        try { 
            Callback = cb;
            return 0;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    ///  Sets the exception callback.
    /// </summary>
    /// <param name="ecb">The exception callback.</param>
    /// <returns>0 on success, -1 on failure.</returns>
    [UnmanagedCallersOnly(EntryPoint = "Gs1_SetExceptionCallback")]
    public static int Gs1_SetExceptionCallback(delegate* unmanaged<int, int, char*, int, int, int, void> ecb) {
        try {
            ExceptionCallback = ecb;
            return 0;
        }
        catch {
            return -1;
        }
    }

    /// <summary>
    /// Parses the provided data.
    /// </summary>
    /// <param name="data">The data to parse.</param>
    /// <param name="length">The length of the data.</param>
    /// <param name="relationshipTests">The relationship tests to apply.</param>
    /// <returns>0 on success, -1 on failure.</returns>
    [UnmanagedCallersOnly(EntryPoint = "Gs1_Parse")]
    public static int Gs1_Parse(char* data, int length, int relationshipTests) {
        try
        {
            var span = new ReadOnlySpan<char>(data, Math.Max(length, 0));
            Parser.Parse(
                span, 
                entity => {
                    if (Callback != null)
                    {
                        var resolvedAi = (ResolvedApplicationIdentifier)entity;
                        fixed (char* idPtr = entity.Identifier)
                        fixed (char* valPtr = entity.Value)
                        fixed (char* dtPtr = entity.DataTitle)
                        fixed (char* descPtr = entity.Description) {
                            Callback(
                                idPtr,
                                resolvedAi.Identifier.Length,
                                valPtr,
                                resolvedAi.Value.Length,
                                resolvedAi.Entity,
                                dtPtr,
                                resolvedAi.DataTitle.Length,
                                descPtr,
                                resolvedAi.Description.Length,
                                resolvedAi.InverseExponent ?? -1,
                                resolvedAi.Sequence ?? -1,
                                resolvedAi.IsFixedWidth ? 1 : 0,
                                resolvedAi.IsError ? 1 : 0,
                                resolvedAi.IsFatal ? 1 : 0,
                                resolvedAi.CharacterPosition,
                                resolvedAi.Index);
                        }
                    }

                    if (ExceptionCallback != null)
                    {
                        foreach (var exception in entity.Exceptions)
                        {
                            fixed (char* msgPtr = exception.Message)
                            {
                                ExceptionCallback(
                                    entity.Entity,
                                    exception.ErrorNumber,
                                    msgPtr,
                                    exception.Message.Length,
                                    exception.IsFatal ? 1 : 0,
                                    exception.Offset);
                            }
                        }
                    }
                },
            initialPosition: 0,
            relationshipTests: (DataRelationshipTests)relationshipTests,
            semantics: default);
            return 0;
        }
        catch
        {
            return -1;
        }
    }
}