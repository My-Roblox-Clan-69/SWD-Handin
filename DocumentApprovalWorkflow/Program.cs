using DocumentApprovalWorkflow.Handlers;

namespace DocumentApprovalWorkflow;

class Program
{
    static void Main(string[] args)
    {
        var hrManager = new HRManager();
        var financeDirector = new FinanceDirector();
        var engineeringLead = new EngineeringLead();
        var ceo = new CEO();

        hrManager.SetNext(financeDirector);
        financeDirector.SetNext(ceo);

        var documents = new List<Document>
        {
            new Document("Employee Onboarding Form", 5, Department.HR, Urgency.Low, new List<string> { "HR Manager" }),
            new Document("Annual Budget", 40, Department.Finance, Urgency.Medium, new List<string> { "Finance Director", "CEO" }),
            new Document("New Product Specs", 25, Department.Engineering, Urgency.High, new List<string> { "Engineering Lead" }),
            new Document("Merger Agreement", 80, Department.Executive, Urgency.High, new List<string> { "Finance Director", "CEO" })
        };

        foreach (var doc in documents)
        {
            Console.WriteLine($"\nProcessing {doc.Title} (Dept: {doc.Department}, Urgency: {doc.Urgency}, Importance: {doc.ImportanceLevel})...");
            var request = new ApprovalRequest(doc);

            Approver startingApprover = doc.Department switch
            {
                Department.HR => hrManager,
                Department.Finance => financeDirector,
                Department.Engineering => engineeringLead,
                _ => ceo
            };

            startingApprover.ProcessRequest(request);

            Console.WriteLine("Approval Log:");
            foreach (var log in request.ApprovalLog)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine($"Status: {(doc.IsApproved ? "Approved" : $"Rejected - {doc.RejectionReason}")}");
        }
    }
}