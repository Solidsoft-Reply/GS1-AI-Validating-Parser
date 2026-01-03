#include <windows.h>
#include <iostream>
#include <string>
#include <vector>

// Entity callback: prints resolved entity details
void __stdcall OnEntity(
    wchar_t* identifier,
    int idLen,
    wchar_t* value,
    int valLen,
    int entity,
    wchar_t* dataTitle,
    int dtLen,
    wchar_t* description,
    int descLen,
    int inverseExponent,
    int sequence,
    int isFixedWidth,
    int isError,
    int isFatal,
    int characterPosition,
    int index)
{
    std::wcout << L"\r\n[Entity]\r\n\tAI='" << std::wstring(identifier, idLen)
               << L"'\r\n\tValue='" << std::wstring(value, valLen)
               << L"'\r\n\tEntity=" << entity
               << L"\r\n\tTitle='" << std::wstring(dataTitle, dtLen)
               << L"'\r\n\tDesc='" << std::wstring(description, descLen)
               << L"'\r\n\tInverseExp=" << inverseExponent
               << L"\r\n\tSeq=" << sequence
               << L"\r\n\tFixedWidth=" << (isFixedWidth ? L"true" : L"false")
               << L"\r\n\tError=" << (isError ? L"true" : L"false")
               << L"\r\n\tFatal=" << (isFatal ? L"true" : L"false")
               << L"\r\n\tPos=" << characterPosition
               << L"\r\n\tIndex=" << index
               << std::endl;
}

// Exception callback: prints exception details
void __stdcall OnException(int entity, int errorNumber, wchar_t* message, int msgLen, int isFatal, int offset)
{
    std::wcout << L"\r\n[Exception]\r\n\tEntity=" << entity
               << L"\r\n\tError=" << errorNumber
               << L"\r\n\tMessage='" << std::wstring(message, msgLen)
               << L"'\r\n\tFatal=" << (isFatal ? L"true" : L"false")
               << L"\r\n\tOffset=" << offset
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
typedef int (__stdcall *ParseFn)(wchar_t*, int, int, int, int, int, const wchar_t** gcps, int count);

int wmain(int argc, wchar_t* argv[])
{
    // Use provided input or default sample
    std::wstring input;
    if (argc > 1) {
        input = argv[1];
    } else {
        input = L"01095011015300061731123110ABC123[GS]21SN000111222333";
    }

    std::wcout << L"\r\n[Input]\t" << input << std::endl;

    input = NormalizeInput(input);

    // Load published native DLL (assumes it’s in PATH or same directory)
    HMODULE hMod = LoadLibraryW(L"Solidsoft.Reply.Parsers.Gs1Ai.Native.win-x64.dll");
    if (!hMod) {
        std::wcerr << L"Failed to load native parser DLL" << std::endl;
        return 1;
    }

    // Resolve exported functions
    auto pSetCb = reinterpret_cast<SetCallbackFn>(GetProcAddress(hMod, "Gs1Ai_SetCallback"));
    auto pSetExCb = reinterpret_cast<SetExceptionCallbackFn>(GetProcAddress(hMod, "Gs1Ai_SetExceptionCallback"));
    auto pParse = reinterpret_cast<ParseFn>(GetProcAddress(hMod, "Gs1Ai_Parse"));

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

    // build GCP array
    std::vector<std::wstring> gcpStorage{ L"012345", L"078129", L"123456"};
    std::vector<const wchar_t*> gcpPtrs;
    gcpPtrs.reserve(gcpStorage.size());
    for (const auto& s : gcpStorage) {
        gcpPtrs.push_back(s.c_str());
    }

    // Parse with relationship tests = All
    int rc = pParse(
        const_cast<wchar_t*>(input.c_str()), 
        static_cast<int>(input.length()), 
		2,  // All relationship tests
        0,  // GTIN
        0,  // Expiry Date of trade item
        0,  // amount payable on Invoice slip
        gcpPtrs.data(),  // GCPs
        static_cast<int>(gcpPtrs.size()));
    if (rc != 0) {
        std::wcerr << L"Parse failed: rc=" << rc << std::endl;
        return rc;
    }

    std::wcout << L"Done." << std::endl;
    return 0;
}
