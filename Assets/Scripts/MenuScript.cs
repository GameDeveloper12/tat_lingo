// MenuScript for Open Source Game by Android Helper Games
// v.1.0
// Если увидите ошибки, то поймите меня, я писал быстро)
// Скрипт на 5 % оптимизирован, так что могут быть баги и т.д.

using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Используем библиотеку UnityEngine.UI для управления интерфейсом (Текст, изображения и т.д).

public class MenuScript : MonoBehaviour {

    public bool Debug_Mode = false; // Если хотите полностью выключить дебаг мод.
    public Image fadeImage; // Картинка для затухания при загрузке.
    public GameObject endPanel; // Панель при окончании игры.
    public Text MoneyRecord; // Текст с рекордом денег.
    public GameObject exitPanel; // Панель при выходе из игры.
    public Toggle debugModeTggl; // Переключатель в меню для включения и выключения дебаг мода.
    private float fadeTime = 4.5f; // Скорость перехода при загрузке.
    private string ended; // Строка где хранится сохранённая информация о окончании игры.
    private int money; // Число где хранится сохранённая информация о кол-ве денег.
    private int debugtggl = 0; // Число с настройкой (Включен ли дебаг [1 - да, 0 - нет]).
    private bool exitActive = false; // Если панель выхода активна, то = true.
    private bool clickedStart = false; // Если нажата кнопка старта, то = true.
    private bool fade = false; // Если нужен переход при загрузке уровня, то = true.

    void Awake () // Данная функция вызывается до инициализации всех остальных скриптов. Обычно используется для установки определенных параметров и инициализации переменных.
    {
        if (PlayerPrefs.HasKey("Ended?")) // Если сохранена информация об окончании игры, то:
        {
            ended = PlayerPrefs.GetString("Ended?"); // Присвоить строке сохранённые данные.
            money = PlayerPrefs.GetInt("MR"); // Присвоить числу сохранённые данные.
        }
        if (ended == "Ended") // Если игра была закончена, то:
        {
            endPanel.SetActive(true); // Включить панель при окончании игры.
        }
        else // Иначе
        {
            endPanel.SetActive(false); // Выключить панель при окончании игры.
        }
        if (PlayerPrefs.HasKey("DebugMode")) // Если сохранена информация об включении дебаг мода, то:
        {
            if (Debug_Mode == false) // Если вы в Editor выключили debug mode, то:
            {
                debugModeTggl.gameObject.SetActive(true); // Выключить объект (Переключатель в меню для включения и выключения дебаг мода.)
                PlayerPrefs.SetInt("DebugMode", 0); // Сохранить число о выключеном дебаг моде.
                PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
            }
            else // Иначе
            {
                debugtggl = PlayerPrefs.GetInt("DebugMode"); // Присвоить числу сохранённые данные об Debug Mode.
            }
        }
        else // Иначе
        {
            PlayerPrefs.SetInt("DebugMode", 0); // Сохранить число о выключенном дебаг моде.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
        }
        MoneyRecord.text = "Правильных ответов: " + money.ToString(); // Присвоить тексту строку.
    }
    void Start() // Вызывается один раз при запуске скрипта.
    {
        if (debugtggl == 1) // Если число debugtggl = 1, то:
        {
            debugModeTggl.isOn = true; // Включить птичку на переключателе в меню.
        }
        else // Иначе
        {
            debugModeTggl.isOn = false; // Выключить птичку на переключателе в меню.
        }
    }
    void Update() // Данная функция вызывается каждый раз перед отображением очередного кадра. Самая используемая для расчетов игровых параметров.
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Если нажата кнопка (Escape) либо кнопка назад на смартфоне, то:
        {
            if (exitActive == false) // Если выключена панель выхода, то:
            {
                exitPanel.SetActive(true); // Включить панель выхода.
                exitActive = true; // (Если панель выхода активна, то = true.)
            }
            else // Иначе
            {
                exitPanel.SetActive(false); // Выключить панель выхода.
                exitActive = false; // (Если панель выхода неактивна, то = false.)
            }
        }
        if (debugModeTggl.isOn) // Если стоит птичка на переключателе в меню, то:
        {
            PlayerPrefs.SetInt("DebugMode", 1); // Изменить ключ в сохранениях на 1.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
        }
        else // Иначе
        {
            PlayerPrefs.SetInt("DebugMode", 0); // Изменить ключ в сохранениях на 0.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
        }
        if (clickedStart == true) // Если нажата кнопка начала игры, то:
        {
            PlayerPrefs.DeleteKey("wQ"); // Удалить ключ числа с нумерацией вопроса.
            PlayerPrefs.DeleteKey("M"); // Удалить ключ сохранённого числа с кол-вом денег.
            PlayerPrefs.DeleteKey("Ended?"); // Удалить ключ о завершении игры.
        }
    }
    void FixedUpdate() // Данная функция вызывается каждый раз при расчете физических показателей. Все расчеты физики следует проводить именно в ней. Но я рассчитываю анимацию перехода, и использую эту функцию для того, чтобы не было никаких тормозов.
    {
        if (fade == true) // Если [Надо ли делать переход при загрузке.] = true, то:
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.black, fadeTime * Time.deltaTime); // Делаем переход из цвета картинки в чёрный.
        }
        if (fadeImage.color == Color.black) // Если цвет картинки = чёрному, то:
        {
            Application.LoadLevel(1); // Загрузить level 1 (Игра)
        }
    }
    public void OnClickStart() // Публичная функция (При нажатии на старт)
    {
        fade = true; // (Если нужен переход при загрузке уровня, то = true).
        clickedStart = true; // (Если нажата кнопка старта, то = true).
    }
    public void OnClickExit() // Публичная функция (При нажатии на выход)
    {
        System.Diagnostics.Process.GetCurrentProcess().Kill(); // Полностью закрываем игру.
    }
    public void OnClickBack() // Публичная функция (При нажатии назад)
    {
        exitPanel.SetActive(false); // Выключить панель выхода.
        exitActive = false; // (Если панель выхода неактивна, то = false).
    }
    public void OnClickContinue() // Публичная функция (При нажатии продолжить)
    {
        PlayerPrefs.DeleteKey("Ended"); // Удалить ключ сохранения об окончании игры.
    }
}