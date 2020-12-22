using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternCommand
{
    class Program
    {
        static void Main()
        {
            var tv = new CommandTv("Телевизор в гостиной");
            var tv2 = new CommandTv("Телевизор в спальне");
            var tv3 = new CommandTv("Телевизор на кухне");
            var light = new CommandLight("Свет в гостиной");
            var light2 = new CommandLight("Свет в спальне");
            var light3 = new CommandLight("Свет на кухне");
            var remoteControl = new RemoteControl();
            remoteControl.AddDevice(1, tv);
            remoteControl.AddDevice(2, light);
            remoteControl.AddDevice(3, tv2);
            remoteControl.AddDevice(4, light2);
            remoteControl.AddDevice(5, tv3);
            remoteControl.AddDevice(6, light3);
            remoteControl.PrintMenu();
            var input = Console.ReadLine();
            while (input != "0")
            {
                if (input != null)
                {
                    var button = Int32.Parse(input);
                    remoteControl.RunCommand(button);
                }
                input = Console.ReadLine();
            }
        }
        public interface ICommand
        {
            void Execute();
            void Undo();
        }
        public enum State
        {
            Off = 0,
            On = 1
        }
        public class Tv
        {
            public void TurnOn()
            {
                Console.WriteLine("Телевизор включен");
                State = State.On;
            }
            public void TurnOf()
            {
                Console.WriteLine("Телевизор выключен");
                State = State.Off;
            }
            public State State { get; set; }
        }
        class CommandTv : ICommand
        {
            private readonly Tv _tv;
            private string _name;
            public CommandTv(String name)
            {
                _tv = new Tv();
                _name = name;
            }
            public CommandTv(Tv tv, String name)
            {
                _tv = tv;
                _name = name;
            }
            public void Execute()
            {
                switch (_tv.State)
                {
                    case State.On:
                        _tv.TurnOf();
                        break;
                    case State.Off:
                        _tv.TurnOn();
                        break;
                }
            }
            public void Undo()
            {
                switch (_tv.State)
                {
                    case State.On:
                        _tv.TurnOf();
                        break;
                    case State.Off:
                        _tv.TurnOn();
                        break;
                }
            }
            public override string ToString()
            {
                return _name;
            }
        }
        public class Light
        {
            public void TurnOn()
            {
                Console.WriteLine("Свет включен");
                State = State.On;
            }
            public void TurnOf()
            {
                Console.WriteLine("Свет выключен");
                State = State.Off;
            }
            public State State { get; set; }
        }
        class CommandLight : ICommand
        {
            private readonly Light _light;
            private string _name;
            public CommandLight(String name)
            {
                _light = new Light();
                _name = name;
            }
            public CommandLight(Light light, String name)
            {
                _light = light;
                _name = name;
            }
            public void Execute()
            {
                switch (_light.State)
                {
                    case State.On:
                        _light.TurnOf();
                        break;
                    case State.Off:
                        _light.TurnOn();
                        break;
                }
            }
            public void Undo()
            {
                switch (_light.State)
                {
                    case State.On:
                        _light.TurnOf();
                        break;
                    case State.Off:
                        _light.TurnOn();
                        break;
                }
            }
            public override string ToString()
            {
                return _name;
            }
        }
        public class RemoteControl
        {
            private readonly Dictionary<int, ICommand> _devices;
            public RemoteControl()
            {
                _devices = new Dictionary<int, ICommand> ();
            }
            public void AddDevice(int id, ICommand device)
            {
                _devices[id] = device;
            }
            public void RunCommand(int id)
            {
                if (_devices.ContainsKey(id))
                {
                    _devices[id].Execute();
                }
            }
            public void RemoveDevice(ICommand device)
            {
                if (_devices.ContainsValue(device))
                {
                    var removeDevice = _devices.FirstOrDefault(x => x.Value == device);
                    _devices.Remove(removeDevice.Key);
                }
            }
            ///
            /// Отображение списка привязанных к кнопкам устройств
            ///
            public void PrintMenu()
            {
                foreach (var command in _devices)
                {
                    Console.WriteLine("{0}: \t {1}", command.Key, command.Value);
                }
                Console.WriteLine("0: \t ВЫХОД");
            }
        }
    }
}
