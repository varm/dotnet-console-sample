namespace dotnet_console_sample.Interface
{
    public interface ISclass
    {
        Task<string> DisplayClassName();
        Task GetCustomerList();
    }
}