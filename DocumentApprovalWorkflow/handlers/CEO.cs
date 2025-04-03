namespace DocumentApprovalWorkflow.Handlers;

public class CEO : Approver
{
    public CEO() : base("CEO", 100, Enum.GetValues(typeof(Department)).Cast<Department>().ToList()) { }
}