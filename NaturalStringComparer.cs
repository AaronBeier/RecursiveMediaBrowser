using System.Runtime.InteropServices;

namespace RecursiveMediaBrowser {

  internal sealed class NaturalStringComparer : IComparer<string> {

    // This code is from https://stackoverflow.com/questions/248603/natural-sort-order-in-c-sharp
    // StrCmpLogicalW Reference:
    // https://learn.microsoft.com/en-us/windows/win32/api/shlwapi/nf-shlwapi-strcmplogicalw
    // https://www.pinvoke.net/default.aspx/shlwapi/StrCmpLogicalW.html

    [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
    public static extern int StrCmpLogicalW(string psz1, string psz2);

    public int Compare(string? A, string? B) {
      return StrCmpLogicalW(A ?? string.Empty, B ?? string.Empty);
    }
  }
}
