using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public enum InputType
    {
        TaskStatusChange = 1,

        ImageId,

        SignatureId,

        UserStatusChange,

        UserComment
    }
}