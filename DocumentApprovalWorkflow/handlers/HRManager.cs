namespace DocumentApprovalWorkflow.Handlers;

public class HRManager : Approver
{
    public HRManager() : base("HR Manager", 20, new List<Department> { Department.HR }) { }

    public override void ProcessRequest(ApprovalRequest request)
    {
        if (request.Document.Department == Department.HR && request.Document.ImportanceLevel <= 10)
        {
            request.Log("HR Manager fast-tracked approval due to low importance.");
            request.Document.RequiredApprovals.Remove(_role);
            request.Document.IsApproved = true;
        }
        else
        {
            base.ProcessRequest(request);
        }
    }
}