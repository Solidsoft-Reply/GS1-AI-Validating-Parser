#include <windows.h>
#include <iostream>
#include <string>

// Entity callback: prints resolved entity details
void __stdcall OnEntity(
    wchar_t* identifier, int idLen,
    wchar_t* value, int valLen,
    int entity,
    wchar_t* dataTitle, int dtLen,
    wchar_t* description, int descLen,
    int inverseExponent,
    int sequence,
    int isFixedWidth,
    int isError,
    int isFatal,
    int characterPosition,
    int index)
{
    std::wcout << L"[Entity] AI='" << std::wstring(identifier, idLen)
               << L"' Value='" << std::wstring(value, valLen)
               << L"' Entity=" << entity
               << L" Title='" << std::wstring(dataTitle, dtLen)
               << L"' Desc='" << std::wstring(description, descLen)
               << L"' InverseExp=" << inverseExponent
               << L" Seq=" << sequence
               << L" FixedWidth=" << (isFixedWidth ? L"true" : L"false")
               << L" Error=" << (isError ? L"true" : L"false")
               << L" Fatal=" << (isFatal ? L"true" : L"false")
               << L" Pos=" << characterPosition
               << L" Index=" << index
               << std::endl;
}

// Exception callback: prints exception details
void __stdcall OnException(int entity, int errorNumber, wchar_t* message, int msgLen, int isFatal, int offset)
{
    std::wcout << L"[Exception] Entity=" << entity
               << L" Error=" << errorNumber
               << L" Message='" << std::wstring(message, msgLen)
               << L"' Fatal=" << (isFatal ? L"true" : L"false")
               << L" Offset=" << offset
               << std::endl;
}

// Replace [GS] with ASCII 29
std::wstring NormalizeInput(const std::wstring& input)
{
    std::wstring s = input;
    const std::wstring token = L"[GS]";
    size_t pos = 0;
    while ((pos = s.find(token, pos)) != std::wstring::npos) {
        s.replace(pos, token.length(), std::wstring(1, wchar_t(29)));
        pos += 1;
    }
    return s;
}

// Function pointer typedefs matching native exports
typedef int (__stdcall *SetCallbackFn)(void(__stdcall*)(wchar_t*, int, wchar_t*, int, int, wchar_t*, int, wchar_t*, int, int, int, int, int, int, int, int));
typedef int (__stdcall *SetExceptionCallbackFn)(void(__stdcall*)(int, int, wchar_t*, int, int, int));
typedef int (__stdcall *ParseFn)(wchar_t*, int, int);

int wmain(int argc, wchar_t* argv[])
{
    // Use provided input or default sample
    std::wstring input;
    if (argc > 1) {
        input = argv[1];
    } else {
        input = L"01095011015300061731123110ABC123[GS]21SN000111222333";
    }

    input = NormalizeInput(input);

    // Load published native DLL (assumes it’s in PATH or same directory)
    HMODULE hMod = LoadLibraryW(L"Solidsoft.Reply.Parsers.Gs1Ai.Native.win-x64.dll");
    if (!hMod) {
        std::wcerr << L"Failed to load native parser DLL" << std::endl;
        return 1;
    }

    // Resolve exported functions
    auto pSetCb = reinterpret_cast<SetCallbackFn>(GetProcAddress(hMod, "Gs1_SetCallback"));
    auto pSetExCb = reinterpret_cast<SetExceptionCallbackFn>(GetProcAddress(hMod, "Gs1_SetExceptionCallback"));
    auto pParse = reinterpret_cast<ParseFn>(GetProcAddress(hMod, "Gs1_Parse"));

    if (!pSetCb || !pSetExCb || !pParse) {
        std::wcerr << L"Failed to resolve one or more exports" << std::endl;
        return 1;
    }

    // Set callbacks
    if (pSetCb(&OnEntity) != 0) {
        std::wcerr << L"Failed to set entity callback" << std::endl;
        return 1;
    }
    if (pSetExCb(&OnException) != 0) {
        std::wcerr << L"Failed to set exception callback" << std::endl;
        return 1;
    }

    // Parse with relationship tests = All
    int rc = pParse(const_cast<wchar_t*>(input.c_str()), static_cast<int>(input.length()), /*relationshipTests*/ 2);
    if (rc != 0) {
        std::wcerr << L"Parse failed: rc=" << rc << std::endl;
        return rc;
    }

    std::wcout << L"Done." << std::endl;
    return 0;
}
