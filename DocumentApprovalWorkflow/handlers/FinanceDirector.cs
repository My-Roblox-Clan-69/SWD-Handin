namespace DocumentApprovalWorkflow.Handlers;

public class FinanceDirector : Approver
{
    public FinanceDirector() : base("Finance Director", 50, new List<Department> { Department.Finance, Department.Executive }) { }
}