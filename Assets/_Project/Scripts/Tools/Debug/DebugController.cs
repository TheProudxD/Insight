using System;
using System.Collections.Generic;
using System.Linq;
using ResourceService;
using UnityEngine;
using Zenject;

namespace Utilites
{
    public class DebugController : MonoBehaviour
    {
        [Inject] private ResourceManager _resourceManager;

        private readonly List<DebugCommandBase> _commands = new();

        private DebugCommand _helpCommand;
        private DebugCommand<int> _giveSoftCurrencyCommand;
        private DebugCommand<int> _giveHardCurrencyCommand;
        private DebugCommand<int> _giveEnergyCommand;
        private bool _showConsole;
        private string _input;
        private bool _showHelp;
        private Vector2 _scroll;

        private void Awake()
        {
            _helpCommand =
                new("help", "Shows help commands", "help",
                    () => _showHelp = true);
            _commands.Add(_helpCommand);

            _giveSoftCurrencyCommand =
                new("give_soft_currency", "Gives the <amount> of the soft currency",
                    "give_soft_currency <amount>",
                    (v) => _resourceManager.Add(ResourceType.SoftCurrency, v));
            _commands.Add(_giveSoftCurrencyCommand);

            _giveHardCurrencyCommand =
                new("give_hard_currency", "Gives the <amount> of the hard currency", "give_hard_currency <amount>",
                    (v) => _resourceManager.Add(ResourceType.HardCurrency, v));
            _commands.Add(_giveHardCurrencyCommand);

            _giveEnergyCommand =
                new("give_energy", "Gives the <amount> of the energy", "give_energy <amount>",
                    (v) => _resourceManager.Add(ResourceType.Energy, v));
            _commands.Add(_giveEnergyCommand);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _showConsole = !_showConsole;
                _input = "";
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Send();
            }
        }

        private void Send()
        {
            HandleInput();
            _input = "";
        }

        private void HandleInput()
        {
            var properties = _input.Split(" ");
            foreach (var command in _commands.Where(command => _input.Contains(command.ID)))
            {
                switch (command)
                {
                    case DebugCommand debugCommand:
                        debugCommand.Invoke();
                        break;
                    case DebugCommand<int> debugCommandInt:
                        debugCommandInt.Invoke(int.Parse(properties.Last()));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(command));
                }
            }
        }

        private void OnGUI()
        {
            if (!_showConsole)
                return;

            var y = 0f;

            if (_showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width - 200, 100), "");

                var viewport = new Rect(0, 0, Screen.width - 200, 20 * _commands.Count);
                _scroll = GUI.BeginScrollView(new Rect(0, y + 5, Screen.width - 200, 90), _scroll, viewport);
                for (var i = 0; i < _commands.Count; i++)
                {
                    var command = _commands[i];
                    var label = $"{command.Format} - {command.Description}";
                    var labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();

                y += 100;
            }

            GUI.Box(new Rect(0, y, Screen.width - 150, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            GUI.color = Color.green;
            _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 180, 20f), _input);

            var sendButtonPressed = GUI.Button(new Rect(Screen.width - 170, y + 5f, 140, 35), "Send");
            if (sendButtonPressed)
            {
                Send();
            }
        }
    }
}