namespace MunCode.Core.AppHosting
{
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextFactory
    {
        DbContext Create();
    }
}