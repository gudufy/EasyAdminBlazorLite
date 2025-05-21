using BootstrapBlazor.Components;
using EasyAdminBlazor;
using EasyAdminBlazor.Components;
using EasyAdminBlazor.Test.Blog;
using EasyAdminBlazor.Test.Components;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.AddEasyAdminBlazor(new EasyAdminBlazorOptions
{
    Assemblies = [typeof(Program).Assembly],
    EnableLocalization = false,
    UseAutoSyncStructure = true,
    DataType= FreeSql.DataType.MySql
});

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddBootstrapBlazorTableExportService();

builder.Services.Configure<HubOptions>(option => {
    option.MaximumReceiveMessageSize = null;
});

var app = builder.Build();

// ���EnableLocalization=true���������ע�ʹ�
//var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
//if (option != null)
//{
//    app.UseRequestLocalization(option.Value);
//}

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddAdditionalAssemblies(typeof(EasyAdminBlazor._Imports).Assembly)
    .AddInteractiveServerRenderMode();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

#region ����ʾ����������
var fsql = app.Services.GetService<IFreeSql>();
if (!fsql.Select<Classify>().Any() &&
    !fsql.Select<SysMenu>().Any(a => new[]
    {
        "Blog/Article",
        "Blog/Comment",
        "Blog/Classify",
        "Blog/Channel",
        "Blog/Tag2",
        "Blog/UserLike",
    }.Contains(a.Path)))
{
    var adminUser = fsql.Select<SysUser>().Where(a => a.Roles.Any(b => b.IsAdministrator)).First();

    List<SysMenu> getCudButtons(params SysMenu[] btns) => new[]
    {
        new SysMenu { Label = "���", Path = "add", Sort = 10011, Type = SysMenuType.��ť, },
        new SysMenu { Label = "�༭", Path = "edit", Sort = 10012, Type = SysMenuType.��ť, },
        new SysMenu { Label = "ɾ��", Path = "remove", Sort = 10013, Type = SysMenuType.��ť, }
    }.Concat(btns ?? new SysMenu[0]).ToList();
    var repo = fsql.GetAggregateRootRepository<SysMenu>();
    repo.Insert(new[]
    {
        new SysMenu
        {
            Label = "����ʾ��",
            Path = "",
            Sort = 998,
            Type = SysMenuType.�˵�,
            Childs = new List<SysMenu>
            {
                new SysMenu { Label = "�������", Path = "Blog/Article", Sort = 10001, Type = SysMenuType.�˵�, Childs = getCudButtons() },
                new SysMenu { Label = "����", Path = "Blog/Comment", Sort = 10002, Type = SysMenuType.�˵�, Childs = getCudButtons() },
                new SysMenu { Label = "���ר��", Path = "Blog/Classify", Sort = 10003, Type = SysMenuType.�˵�, Childs = getCudButtons() },
                new SysMenu { Label = "����Ƶ��", Path = "Blog/Channel", Sort = 10004, Type = SysMenuType.�˵�, Childs = getCudButtons() },
                new SysMenu { Label = "��ǩ", Path = "Blog/Tag2", Sort = 10006, Type = SysMenuType.�˵�, Childs = getCudButtons() },
                new SysMenu { Label = "�û�����", Path = "Blog/UserLike", Sort = 10007, Type = SysMenuType.�˵�, Childs = getCudButtons() },
            }
        },
    });

    fsql.Insert(new[]
    {
        new Classify { Id = 510337284071493, ClassifyName = "FreeSql", CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Classify { Id = 510337332621381, ClassifyName = "FreeRedis", CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Classify { Id = 510337373491269, ClassifyName = "FreeScheduler", CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Classify { Id = 510337418735685, ClassifyName = "CSRedis", CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Classify { Id = 510337460719685, ClassifyName = "AdminBlazor", CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
    }).ExecuteAffrows();

    fsql.Insert(new[]
    {
        new Channel { Id = 510338108866629, ChannelName = ".NET", ChannelCode = "net", Remark = ".NET����Ƶ��", Status = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Channel { Id = 510338191179845, ChannelName = "ǰ��", ChannelCode = "html", Remark = "ǰ�˼���Ƶ��", Status = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Channel { Id = 510338291052613, ChannelName = "���ݿ�", ChannelCode = "db", Remark = "���ݿ⼼��Ƶ��", Status = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
    }).ExecuteAffrows();

    fsql.Insert(new[]
    {
        new Tag2 { Id = 510340412510277, TagName = "orm", Remark =  "orm ��������", Status = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Tag2 { Id = 510340482543685, TagName = "js", Remark =  "js �й�����", Status = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Tag2 { Id = 510340574564421, TagName = "vue", Remark =  "vue �й�����", Status = false, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new Tag2 { Id = 510340626989125, TagName = "react", Remark = "react ����", Status = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
    }).ExecuteAffrows();

    fsql.Insert(new[]
    {
        new Tag2.ChannelTag2 { ChannelId = 510338108866629, TagId = 510340412510277 },
        new Tag2.ChannelTag2 { ChannelId = 510338291052613, TagId = 510340412510277 },
        new Tag2.ChannelTag2 { ChannelId = 510338191179845, TagId = 510340482543685 },
        new Tag2.ChannelTag2 { ChannelId = 510338191179845, TagId = 510340574564421 },
        new Tag2.ChannelTag2 { ChannelId = 510338191179845, TagId = 510340626989125 },
    }).ExecuteAffrows();


    fsql.Insert(new[]
    {
        new Article {
            Id = 510359705468997, ClassifyId = 510337460719685, ChannelId = 510338191179845,
            Title = "�����׸�֧�� AOT ������ ORM?", Excerpt = "...",
            Content = "FreeSql\r\n��һ���ǿ��Ķ����ϵӳ�䣨O/RM�������֧�� .NET Core 2.1+��.NET Framework 4.0+��Xamarin�������׸�֧�� AOT ������ ORM?\r\n\r\n�����ĵ� ??��Ƶ�̳� ??\r\n\r\n����������\r\n??�Կ�����Ϊ���ĵ��������������룬����������\r\n\r\n�ೡ��ʵ��\r\n?? ֧�� CodeFirst / DbFirst / DbContext / Repository / UnitOfWork / AOP / ֧�� .NETCore 2.1+, .NETFramework 4.0+, AOT, Xamarin\r\n\r\n�����ݿ�֧��\r\n??MySql��SqlServer��PostgreSQL��Oracle��Sqlite��Firebird�����Ρ��˴��֡��ϴ�ͨ�á���ȡ����ۡ����ߡ�ClickHouse��QuestDB��Access �����ݿ�\r\n\r\n�ḻ�ı��ʽ����\r\n? ֧�� �ḻ�ı��ʽ�������Լ������Զ��������\r\n\r\nDbFirst\r\n?? ֧�� DbFirst ģʽ��֧�ִ����ݿ⵼��ʵ���࣬��ʹ��ʵ�������ɹ�������ʵ���ࣻ\r\n\r\n����ӳ��\r\n? ֧�� ���������ӳ�䣬���� Pgsql ���������͵ȣ�\r\n\r\n��������\r\n?? ֧�� ��������һ�Զࡢ��Զ�̰�����أ��Լ���ʱ���أ�\r\n\r\n��д����\r\n?? ֧�� ��д���롢�ֱ�ֿ⡢���������ֹ�������������", IsAudit = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username
        },
    }).ExecuteAffrows();

    fsql.Insert(new[]
    {
        new Tag2.TagArticle { ArticleId = 510359705468997, TagId = 510340412510277 },
        new Tag2.TagArticle { ArticleId = 510359705468997, TagId = 510340574564421 },
    }).ExecuteAffrows();

    fsql.Insert(new[]
    {
        new Comment { Id = 510365667639365, ArticleId = 510359705468997, Text = "�ǳ��á�����~~", IsAudit = true, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
    }).ExecuteAffrows();

    fsql.Insert(new[]
    {
        new UserLike { Id = 510365571252293, SubjectId = 510359705468997, SubjectType = UserLikeSubjectType.�������, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
        new UserLike { Id = 510366600106053, SubjectId = 510365667639365, SubjectType = UserLikeSubjectType.��������, CreatedUserId = adminUser.Id, CreatedUserName = adminUser.Username },
    }).ExecuteAffrows();

}
#endregion

app.Run();