using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class Event
    {
        public Event()
        { }

        public Event(Event eventItem)
        {
            Id = eventItem.Id;
            Text = eventItem.Text;
            Time = eventItem.Time;
            UserId = eventItem.UserId;
            InputType = eventItem.InputType;
            RowStatus = eventItem.RowStatus;
            EventType = eventItem.EventType;
            TaskId = eventItem.TaskId;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
        public int InputType { get; set; }
        public int RowStatus { get; set; }
        public int EventType { get; set; }
        public int TaskId { get; set; }
    }
}