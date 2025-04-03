namespace DocumentApprovalWorkflow;

public class ApprovalRequest
{
    public Document Document { get; }
    public List<string> ApprovalLog { get; } = new List<string>();
    public List<string> RequiredApprovals { get; }
    public bool IsApproved { get; set; }
    public string RejectionReason { get; set; }

    public ApprovalRequest(Document document, List<string> requiredApprovals)
    {
        Document = document;
        RequiredApprovals = requiredApprovals;
        IsApproved = false;
        RejectionReason = string.Empty;
    }

    public void Log(string message)
    {
        ApprovalLog.Add($"{DateTime.Now:HH:mm:ss} - {message}");
    }
}