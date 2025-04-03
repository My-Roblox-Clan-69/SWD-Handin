namespace DocumentApprovalWorkflow;

public interface IApproverFactory
{
    IApprover CreateChain();
}