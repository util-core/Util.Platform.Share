using Util;
using Util.Logging.Serilog;
using Util.Ui.NgZorro;

//����WebӦ�ó���������
var builder = WebApplication.CreateBuilder( args );

//����Util����
builder.AsBuild()
    .AddNgZorro( t => t.EnableGenerateAllHtml = true )
    .AddSerilog()
    .AddUtil();

//����WebӦ�ó���
var app = builder.Build();

//��������ܵ�
app.UseNgZorro( 18666 );

//����Ӧ��
app.Run();