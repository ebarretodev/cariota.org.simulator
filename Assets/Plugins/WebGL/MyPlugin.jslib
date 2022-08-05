mergeInto(LibraryManager.library, {
  initialized: function () {
    window.dispatchReactUnityEvent(
      "initialized"
    );
  },
  finishQuest: function () {
    window.dispatchReactUnityEvent(
      "finishQuest"
    );
  }
});