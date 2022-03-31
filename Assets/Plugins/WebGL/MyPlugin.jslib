mergeInto(LibraryManager.library, {
  initialized: function (userName, score) {
    window.dispatchReactUnityEvent(
      "initialized"
    );
  },
});