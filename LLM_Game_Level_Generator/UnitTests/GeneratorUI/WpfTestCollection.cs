namespace UnitTests
{
    /// <summary>
    /// Collection definition to serialize all WPF-dependent tests.
    /// WPF requires STA threads and a single Application instance,
    /// so these tests must not run in parallel.
    /// </summary>
    [CollectionDefinition("WPF")]
    public class WpfTestCollection
    {
    }
}
