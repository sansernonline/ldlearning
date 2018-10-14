using SQLite4Unity3d;

public class User  {
    
    [PrimaryKey]
	public string Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public int Status { get; set; }
	public string Tel { get; set; }
	public string Token { get; set; }
	public string Coin { get; set; }
	public string CreateDate { get; set; }
	public string UpdateDate { get; set; }
	public string CreateBy { get; set; }
	public string UpdateBy { get; set; }

}
