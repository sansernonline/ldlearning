using SQLite4Unity3d;

public class UserCourseScore  {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
	public int CourseDetailId { get; set; }
	public string UserId { get; set; }
	public int MyScore { get; set; }
	public int CourseScore { get; set; }
	public int LearningTime { get; set; }
	public int LearningUnit { get; set; }
	public int Status { get; set; }
	public int Version { get; set; }
	public string CreateDate { get; set; }

}
