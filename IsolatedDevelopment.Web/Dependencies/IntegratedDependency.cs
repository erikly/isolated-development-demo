namespace IsolatedDevelopment.Web.Dependencies
{
    public class IntegratedDependency : IIntegratedDependency
    {
        public string GetMessage() => $"This message is from {nameof(IntegratedDependency)}";
    }
}
