using System.Runtime.InteropServices;

namespace RecursiveMediaBrowser {

  internal sealed partial class NaturalStringComparer : IComparer<string> {

    // This code is from https://stackoverflow.com/questions/248603/natural-sort-order-in-c-sharp
    // StrCmpLogicalW Reference:
    // https://learn.microsoft.com/en-us/windows/win32/api/shlwapi/nf-shlwapi-strcmplogicalw
    // https://www.pinvoke.net/default.aspx/shlwapi/StrCmpLogicalW.html

    [LibraryImport("shlwapi.dll", EntryPoint = "StrCmpLogicalW", StringMarshalling = StringMarshalling.Utf16)]
    public static partial int StrCmpLogicalW(string psz1, string psz2);

    public int Compare(string? A, string? B) {
      return StrCmpLogicalW(A ?? string.Empty, B ?? string.Empty);
    }
  }
}
