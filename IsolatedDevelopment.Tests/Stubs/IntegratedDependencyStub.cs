using IsolatedDevelopment.Dependencies;

namespace IsolatedDevelopment.Tests.Stubs
{
    class IntegratedDependencyStub : IIntegratedDependency
    {
        public string GetMessage() => $"This message is from {nameof(IntegratedDependencyStub)}";
    }
}
