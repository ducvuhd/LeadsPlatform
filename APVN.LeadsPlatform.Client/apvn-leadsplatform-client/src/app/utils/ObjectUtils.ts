class Describer {
  static describe(instance): Array<string> {
    return Object.getOwnPropertyNames(instance);
  }
}
