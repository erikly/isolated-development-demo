namespace IsolatedDevelopment.Dependencies
{
    public class IntegratedDependency : IIntegratedDependency
    {
        public string GetMessage() => $"This message is from {nameof(IntegratedDependency)}";
    }
}
