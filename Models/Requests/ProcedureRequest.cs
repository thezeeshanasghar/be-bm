namespace dotnet.Models
{
    public class ProcedureRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Executant { get; set; }
        public int Charges { get; set; }
        public int ExecutantShare { get; set; }
    }
}
