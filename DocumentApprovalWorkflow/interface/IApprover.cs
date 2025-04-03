namespace DocumentApprovalWorkflow;

public interface IApprover
{
    void SetNext(IApprover nextApprover);
    void ProcessRequest(ApprovalRequest request);
}