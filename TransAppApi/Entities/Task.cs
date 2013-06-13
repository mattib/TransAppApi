using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class Task
    {
        public Task()
        { }

        public Task(Task task)
        {
            Id = task.Id;
            DeliveryNumber = task.DeliveryNumber;
            User = new User(task.User);
            Company = new Company(task.Company);
            SenderAddress = new Address(task.SenderAddress);
            ReciverAddress = new Address(task.ReciverAddress);
            TaskStatus = task.TaskStatus;
            Created = task.Created;
            PickedUpAt = task.PickedUpAt;
            DeliveredAt = task.DeliveredAt;
            PickUpTime = task.PickUpTime;
            DeliveryTime = task.DeliveryTime;
            LastModified = task.LastModified;
            Comment = task.Comment;
            Contact = new Contact(task.Contact);
            RowStatus = task.RowStatus;
            TaskType = task.TaskType;
            DataExtention = task.DataExtention;
        }

        public int Id { get; set; }
        public string DeliveryNumber { get; set; }
        public User User { get; set; }
        public Company Company { get; set; }
        public Address SenderAddress { get; set; }
        public Address ReciverAddress { get; set; }
        public int TaskStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime LastModified { get; set; }
        public string Comment { get; set; }
        public Contact Contact { get; set; }
        public int RowStatus { get; set; }
        public int? TaskType { get; set; }
        public string DataExtention { get; set; }
    }
}