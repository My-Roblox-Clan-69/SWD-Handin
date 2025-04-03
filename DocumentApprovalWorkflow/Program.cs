namespace DocumentApprovalWorkflow;

class Program
{
    static void Main(string[] args)
    {
        IApproverFactory factory = new ApproverFactory();
        var chain = factory.CreateChain();

        var documents = new List<Document>
        {
            new Document("Employee Onboarding Form", 5, Department.HR, Urgency.Low),
            new Document("Annual Budget", 40, Department.Finance, Urgency.Medium),
            new Document("New Product Specs", 25, Department.Engineering, Urgency.High),
            new Document("Merger Agreement", 80, Department.Executive, Urgency.High)
        };

        var approvalRequests = new List<ApprovalRequest>
        {
            new ApprovalRequest(documents[0], new List<string> { "HR Manager" }),
            new ApprovalRequest(documents[1], new List<string> { "Finance Director", "CEO" }),
            new ApprovalRequest(documents[2], new List<string> { "Engineering Lead" }),
            new ApprovalRequest(documents[3], new List<string> { "Finance Director", "CEO" })
        };

        foreach (var request in approvalRequests)
        {
            Console.WriteLine($"\nProcessing {request.Document.Title} (Dept: {request.Document.Department}, Urgency: {request.Document.Urgency}, Importance: {request.Document.ImportanceLevel})...");
            chain.ProcessRequest(request);

            Console.WriteLine("Approval Log:");
            foreach (var log in request.ApprovalLog)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine($"Status: {(request.IsApproved ? "Approved" : $"Rejected - {request.RejectionReason}")}");
        }
    }
}