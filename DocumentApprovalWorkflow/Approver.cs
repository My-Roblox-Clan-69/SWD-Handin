namespace DocumentApprovalWorkflow;

public abstract class Approver
{
    protected Approver _nextApprover;
    protected string _role;
    protected int _maxImportance;
    protected List<Department> _allowedDepartments;

    public Approver(string role, int maxImportance, List<Department> allowedDepartments)
    {
        _role = role;
        _maxImportance = maxImportance;
        _allowedDepartments = allowedDepartments;
    }

    public void SetNext(Approver nextApprover)
    {
        _nextApprover = nextApprover;
    }

    public virtual void ProcessRequest(ApprovalRequest request)
    {
        var doc = request.Document;

        if (!doc.RequiredApprovals.Contains(_role))
        {
            request.Log($"{_role} skipped - not required for {doc.Title}.");
            PassToNext(request);
            return;
        }

        if (!_allowedDepartments.Contains(doc.Department))
        {
            request.Log($"{_role} rejected {doc.Title} - wrong department ({doc.Department}).");
            doc.RejectionReason = $"Department mismatch for {_role}";
            return;
        }

        if (doc.ImportanceLevel > _maxImportance)
        {
            request.Log($"{_role} cannot approve {doc.Title} (Importance: {doc.ImportanceLevel} > {_maxImportance}). Escalating.");
            PassToNext(request);
            return;
        }

        if (doc.Urgency == Urgency.High && _maxImportance < 50)
        {
            request.Log($"{_role} cannot approve {doc.Title} - High urgency requires senior approval.");
            PassToNext(request);
            return;
        }

        request.Log($"{_role} approved {doc.Title}.");
        doc.RequiredApprovals.Remove(_role);

        if (doc.RequiredApprovals.Count == 0)
        {
            doc.IsApproved = true;
            request.Log($"{doc.Title} fully approved!");
        }
        else
        {
            PassToNext(request);
        }
    }

    protected void PassToNext(ApprovalRequest request)
    {
        if (_nextApprover != null)
        {
            _nextApprover.ProcessRequest(request);
        }
        else if (!request.Document.IsApproved)
        {
            request.Document.RejectionReason = "No further approvers available.";
            request.Log($"{request.Document.Title} rejected - incomplete approvals.");
        }
    }
}