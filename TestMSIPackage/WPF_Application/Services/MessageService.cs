using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Application.Services
{
    internal class ConfirmationMessageService : PubSubEvent<string> { }
    internal class InformationMessageService : PubSubEvent<string> { }
    internal class ErrorMessageService : PubSubEvent<string> { }
}
