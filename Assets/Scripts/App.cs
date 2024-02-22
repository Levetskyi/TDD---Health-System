using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class App : MonoBehaviour
{
    [Header("SetUp")]
    [SerializeField] private int healAmount = 1;
    [SerializeField] private int damageAmount = 1;

    [SerializeField] private List<Image> images;

    private HeartContainer _heartContainer;
    private Player _player;

    private void Awake()
    {
        _player = new Player(20, 20);

        _heartContainer = new HeartContainer(
            images.Select(image => new Heart(image)).ToList());

        _player.Healed += (sender, args) => _heartContainer.Replenish(args.Amount);
        _player.Damaged += (sender, args) => _heartContainer.Deplete(args.Amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _player.Heal(healAmount);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _player.Damage(damageAmount);
        }
    }
}