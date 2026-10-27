using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string Layout = """
<!DOCTYPE html>
<html lang="uk">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>@@TITLE@@</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }
        body {
            font-family: "Segoe UI", system-ui, sans-serif;
            background: linear-gradient(135deg, #0f172a, #1e1b4b);
            color: #e2e8f0;
            min-height: 100vh;
        }
        nav {
            display: flex;
            gap: 8px;
            padding: 16px 32px;
            background: rgba(15, 23, 42, 0.8);
            border-bottom: 1px solid #334155;
        }
        nav a {
            color: #cbd5e1;
            text-decoration: none;
            padding: 8px 18px;
            border-radius: 999px;
            transition: background 0.2s;
        }
        nav a:hover { background: #334155; }
        nav a.active { background: #8b5cf6; color: #fff; }
        main { max-width: 860px; margin: 48px auto; padding: 0 24px; }
        h1 {
            font-size: 2.6rem;
            margin-bottom: 16px;
            background: linear-gradient(90deg, #f472b6, #22d3ee);
            -webkit-background-clip: text;
            background-clip: text;
            color: transparent;
        }
        p { line-height: 1.7; margin-bottom: 16px; color: #cbd5e1; }
        .cards { display: grid; grid-template-columns: repeat(auto-fit, minmax(220px, 1fr)); gap: 16px; margin: 32px 0; }
        .card {
            background: #1e293b;
            border: 1px solid #334155;
            border-radius: 16px;
            padding: 20px;
        }
        .card h3 { margin-bottom: 8px; color: #f8fafc; }
        .button {
            display: inline-block;
            margin-top: 8px;
            padding: 12px 24px;
            border-radius: 12px;
            background: #22d3ee;
            color: #0f172a;
            font-weight: 600;
            text-decoration: none;
        }
        .button:hover { background: #67e8f9; }
        ul { margin: 0 0 16px 24px; line-height: 1.9; color: #cbd5e1; }
        footer { text-align: center; padding: 32px; color: #64748b; font-size: 0.9rem; }
    </style>
</head>
<body>
    <nav>
        <a href="/" class="@@HOME@@">Головна</a>
        <a href="/about" class="@@ABOUT@@">Про мене</a>
    </nav>
    <main>
        @@BODY@@
    </main>
    <footer>ASP.NET Core · перше знайомство</footer>
</body>
</html>
""";

IResult Page(string title, string body, string active) =>
    Results.Content(
        Layout
            .Replace("@@TITLE@@", title)
            .Replace("@@BODY@@", body)
            .Replace("@@HOME@@", active == "home" ? "active" : "")
            .Replace("@@ABOUT@@", active == "about" ? "active" : ""),
        "text/html",
        Encoding.UTF8);

app.MapGet("/", () => Page(
    "Головна",
    """
    <h1>Вітаю на головній сторінці</h1>
    <p>Це перший розділ мого сайту на ASP.NET Core. Сервер повертає браузеру
       не просто рядок тексту, а повноцінну HTML-сторінку зі стилями.</p>
    <div class="cards">
        <div class="card"><h3>Minimal API</h3><p>Маршрути описані прямо в Program.cs.</p></div>
        <div class="card"><h3>HTML</h3><p>Відповідь має тип text/html.</p></div>
        <div class="card"><h3>Навігація</h3><p>Меню вгорі веде між розділами.</p></div>
    </div>
    <a class="button" href="/about">Перейти до розділу «Про мене» →</a>
    """,
    "home"));

app.MapGet("/about", () => Page(
    "Про мене",
    """
    <h1>Про мене</h1>
    <p>Це другий розділ сайту. Тут можна розповісти трохи про себе.</p>
    <ul>
        <li>Вчуся створювати веб-застосунки на React та ASP.NET Core</li>
        <li>Люблю котів і програмування</li>
        <li>Це моя перша сторінка на ASP.NET Core</li>
    </ul>
    <a class="button" href="/">← Повернутися на головну</a>
    """,
    "about"));

app.Run();