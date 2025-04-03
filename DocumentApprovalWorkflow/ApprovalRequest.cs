namespace DocumentApprovalWorkflow;

public class ApprovalRequest
{
    public Document Document { get; }
    public List<string> ApprovalLog { get; } = new List<string>();

    public ApprovalRequest(Document document)
    {
        Document = document;
    }

    public void Log(string message)
    {
        ApprovalLog.Add($"{DateTime.Now:HH:mm:ss} - {message}");
    }
}