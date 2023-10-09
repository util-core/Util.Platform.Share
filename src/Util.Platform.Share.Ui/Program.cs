using Util;
using Util.Logging.Serilog;
using Util.Ui.NgZorro;

//����WebӦ�ó���������
var builder = WebApplication.CreateBuilder( args );

//����Util����
builder.AsBuild()
    .AddNgZorro( t => {
        t.EnableI18n = true;
        t.EnableDefaultOptionText = true;
        t.GenerateHtmlBasePath = "/ClientApp";
        t.GenerateHtmlSuffix = "html";
    } )
    .AddSerilog()
    .AddUtil();

//����WebӦ�ó���
var app = builder.Build();

//��������ܵ�
app.UseNgZorro( "http://localhost:18666" );

//����Ӧ��
app.Run();