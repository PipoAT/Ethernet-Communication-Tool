using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using AT_SCC;

namespace Ethernet_Communication_Tool;

public partial class Form1 : Form
{
    
    private readonly DisplayHelp _displayHelp;

    readonly Button buttonPing = new();
    readonly Button buttonSend = new();
    readonly Button buttonClear = new();
    readonly TextBox textBoxStatus = new();
    readonly TextBox textBoxDataRx = new();
    readonly TextBox textBoxPort = new();
    readonly TextBox textBoxIP = new();

    public Form1()
    {
        InitializeComponent();
        // initialize the DisplayHelp to access UI functions
        _displayHelp = new DisplayHelp();

        // setup the form itself
        Size = ClientSize;
        ControlBox = true;
        MaximizeBox = false;
        MinimizeBox = true;

        _displayHelp.AddLabel("Enter IP Address", new Point(10,10), new Font("Arial", 8), this);
        _displayHelp.SetTextBox(textBoxIP, new Point(10, 30), 150, "", Color.LightYellow, false, this);

        _displayHelp.AddLabel("Enter Port", new Point(180,10), new Font("Arial", 8), this);
        _displayHelp.SetTextBox(textBoxPort, new Point(180, 30), 150, "", Color.LightYellow, false, this);

        _displayHelp.SetButtons(buttonPing, new Point(10, 60), "PING IP ADDRESS", Color.LightGreen, new EventHandler(SendPing), this);
        _displayHelp.SetButtons(buttonSend, new Point(180, 60), "SEND DATA", Color.LightGreen, new EventHandler(SendData), this);
        _displayHelp.SetButtons(buttonClear, new Point(350, 60), "CLEAR DATA", Color.LightGreen, new EventHandler(ClearData), this);


        _displayHelp.SetTextBox(textBoxStatus, new Point(10, 110), 150, "READY", Color.LightYellow, true, this);
        _displayHelp.SetTextBox(textBoxDataRx, new Point(180, 110), 150, "READY", Color.LightYellow, true, this);

    }

    private void ClearData(object? sender, EventArgs e)
    {
        textBoxDataRx.Text = "READY";
        textBoxStatus.Text = "READY";
    }

    private void SendPing(object? sender, EventArgs e) {
        string ipAddress = textBoxIP.Text.ToString();
        Ping pingSender = new();
        PingReply reply = pingSender.Send(ipAddress);
        textBoxStatus.Text = reply.Status.ToString();
    }

    private void SendData(object? sender, EventArgs e) {
        string ipAddress = textBoxIP.Text.ToString();
        int port = Convert.ToInt16(textBoxPort.Text);
        string data = "Hello, World!";

        try 
        {
            TcpClient client = new(ipAddress, port);
            NetworkStream stream = client.GetStream();

            byte[] bytesToSend = Encoding.ASCII.GetBytes(data);

            try
            {
                stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception ex)
            {
                textBoxDataRx.Text = ex.Message;
                return;
            }

            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead;
            try
            {
                bytesRead = stream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            }
            catch (Exception ex)
            {
                textBoxDataRx.Text = ex.Message;
                return;
            }

            string rxData = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            textBoxDataRx.Text = rxData;

            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            textBoxDataRx.Text = ex.Message;
            return;
        }
    }

}
