using System;
using Game.SimulationModule;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class SimulationUI : MonoBehaviour
    {
        [SerializeField] private Button _simulateButton;
        [SerializeField] private Simulation _simulation;
        

        private void Awake()
        {
            _simulateButton.onClick.AddListener(SimulateButtonClicked);
        }

        private void SimulateButtonClicked()
        {
            _simulation.Simulate();
        }
    }
}