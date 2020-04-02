using IsolatedDevelopment.Dependencies;

namespace IsolatedDevelopment.Tests.Setup
{
    class IntegratedDependencyStub : IIntegratedDependency
    {
        public string GetMessage() => $"This message is from {nameof(IntegratedDependencyStub)}";
    }
}
