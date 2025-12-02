namespace Calculator;

public partial class CalculatorApp : Form
{
    private readonly SynchronizationContext? _main;
    public CalculatorApp()
    {
        _main = SynchronizationContext.Current;
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b))
        {
            Task.Run(() => LongAdd(a, b))
                .ContinueWith(pt=>_main.Send(UpdateAnswer, pt.Result));
            //int result = LongAdd(a, b);
            //UpdateAnswer(result);
        }
    }

    private void UpdateAnswer(object? result)
    {
        lblAnswer.Text = result?.ToString();
    }

    private int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
}