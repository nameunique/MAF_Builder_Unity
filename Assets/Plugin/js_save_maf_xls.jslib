
mergeInto(LibraryManager.library, {
  ExportMAFsToXLS: function(js_req)
  {
    _string_js = UTF8ToString(js_req);
    ExportMAFsToXLS_JS(_string_js);
  },
});