namespace XmLesson12_Lab6.Models;

public class LearnerListViewModel
{
    public IReadOnlyList<Learner> Learners { get; init; } = [];
    public int? MajorId { get; init; }
    public string? Keyword { get; init; }
    public bool UseAjax { get; init; } = true;
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 3;
    public int TotalCount { get; init; }
    public int PageCount => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
