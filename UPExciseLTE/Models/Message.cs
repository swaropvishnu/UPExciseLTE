using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class Message
    {
        public string TextMessage { get; set; }
        public string MStatus { get; set; }
        public static Message MsgSuccess(string TextMsg)
        {
            Message Msg = new Message();
            Msg.TextMessage = TextMsg;
            Msg.MStatus = "Success";
            return Msg;
        }
        public static  Message MsgDanger(string TextMsg)
        {
            Message Msg = new Message();
            Msg.TextMessage = TextMsg;
            Msg.MStatus = "Danger";
            return Msg;
        }
        public static Message MsgInfo(string TextMsg)
        {
            Message Msg = new Message();
            Msg.TextMessage = TextMsg;
            Msg.MStatus = "Info";
            return Msg;
        }
        public static Message MsgWarning(string TextMsg)
        {
            Message Msg = new Message();
            Msg.TextMessage = TextMsg;
            Msg.MStatus = "Warning";
            return Msg;
        }
    }
}