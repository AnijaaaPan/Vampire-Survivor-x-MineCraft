using UnityEngine;

[System.Serializable]
class PlayMapInfo
{
    public bool x_axis = false;
    public bool y_axis = false;
    public float? MapXId = null;
    public float? MapYId = null;
    public GameObject LeftTopMap;
    public GameObject RightTopMap;
    public GameObject LeftLowMap;
    public GameObject RightLowMap;
}

public class MoveMapBackGround : MonoBehaviour
{
    public GameObject Chara;
    public GameObject Grid;

    private PlayMapInfo PlayMapInfo = new PlayMapInfo();
    private GameObject LeftTopMap;
    private GameObject RightTopMap;
    private GameObject LeftLowMap;
    private GameObject RightLowMap;

    private Json.PlayerData player = Json.instance.Load();
    private MapDataBase MapDataBase = Json.instance.MapDataBase;
    private const int MapSize = 64;

    void Start()
    {
        Map map = MapDataBase.FindMapFromId(player.Latest_Map);
        Music.instance.PlayMusic(map.GetMusic());
        GameObject SelectMap = Grid.transform.Find($"map_{player.Latest_Map}").gameObject;

        PlayMapInfo.x_axis = map.GetId() != 2;
        PlayMapInfo.y_axis = map.GetId() != 4;

        SetInstantiateObject(SelectMap);
        SetParentObject(RightTopMap);
        SetParentObject(LeftTopMap);
        SetParentObject(RightLowMap);
        SetParentObject(LeftLowMap);

        PlayMapInfo.LeftTopMap = LeftTopMap;
        PlayMapInfo.RightTopMap = RightTopMap;
        PlayMapInfo.LeftLowMap = LeftLowMap;
        PlayMapInfo.RightLowMap = RightLowMap;
    }

    void Update()
    {
        if (!isPlaying.instance.isPlay()) return;

        float getAbsX = Mathf.Abs(Chara.transform.position.x % MapSize);
        if (Chara.transform.position.x < 0)
        {
            getAbsX = MapSize - getAbsX;
        }
        float getAbsY = Mathf.Abs(Chara.transform.position.y % MapSize);
        if (Chara.transform.position.y < 0)
        {
            getAbsY = MapSize - getAbsY;
        }

        int getMapXId = (int)Mathf.Floor(getAbsX / (MapSize / 2));
        int getMapYId = (int)Mathf.Floor(getAbsY / (MapSize / 2));

        if (PlayMapInfo.MapXId != getAbsX || PlayMapInfo.MapYId != getAbsY)
        {
            PlayMapInfo.MapXId = getMapXId;
            PlayMapInfo.MapYId = getMapYId;

            UpdateBackGroundMap();
        }
    }

    private void SetInstantiateObject(GameObject SelectMap)
    {
        SelectMap.SetActive(true);

        RightTopMap = Instantiate(SelectMap);
        if (PlayMapInfo.x_axis == true)
        {
            RightLowMap = Instantiate(SelectMap);
        }

        if (PlayMapInfo.y_axis == true)
        {
            LeftTopMap = Instantiate(SelectMap);
        }

        if (PlayMapInfo.x_axis == true && PlayMapInfo.y_axis == true)
        {
            LeftLowMap = Instantiate(SelectMap);
        }
        SelectMap.SetActive(false);
    }

    private void SetParentObject(GameObject Object)
    {
        if (!Object) return;
        Object.transform.SetParent(this.gameObject.transform);
    }

    private void UpdateBackGroundMap()
    {
        float getFloorX = Mathf.Floor(Chara.transform.position.x / MapSize);
        float getFloorY = Mathf.Floor(Chara.transform.position.y / MapSize);

        float getNowX = getFloorX * MapSize;
        float getNowY = getFloorY * MapSize;
        float setX = getNowX + MapSize;
        float setY = getNowY + MapSize;
        float setMinusX = getNowX - MapSize;
        float setMinusY = getNowY - MapSize;

        if (PlayMapInfo.MapXId == 0 && PlayMapInfo.MapYId == 0)
        {
            SetMapPosition(RightTopMap, new Vector3(getNowX, getNowY, 0));
            SetMapPosition(LeftTopMap, new Vector3(setMinusX, getNowY, 0));
            SetMapPosition(RightLowMap, new Vector3(getNowX, setMinusY, 0));
            SetMapPosition(LeftLowMap, new Vector3(setMinusX, setMinusY, 0));
        }
        else if (PlayMapInfo.MapXId == 1 && PlayMapInfo.MapYId == 0)
        {
            SetMapPosition(RightTopMap, new Vector3(setX, getNowY, 0));
            SetMapPosition(LeftTopMap, new Vector3(getNowX, getNowY, 0));
            SetMapPosition(RightLowMap, new Vector3(setX, setMinusY, 0));
            SetMapPosition(LeftLowMap, new Vector3(getNowX, setMinusY, 0));
        }
        else if (PlayMapInfo.MapXId == 0 && PlayMapInfo.MapYId == 1)
        {
            SetMapPosition(RightTopMap, new Vector3(getNowX, setY, 0));
            SetMapPosition(LeftTopMap, new Vector3(setMinusX, setY, 0));
            SetMapPosition(RightLowMap, new Vector3(getNowX, getNowY, 0));
            SetMapPosition(LeftLowMap, new Vector3(setMinusX, getNowY, 0));
        }
        else
        {
            SetMapPosition(RightTopMap, new Vector3(setX, setY, 0));
            SetMapPosition(LeftTopMap, new Vector3(getNowX, setY, 0));
            SetMapPosition(RightLowMap, new Vector3(setX, getNowY, 0));
            SetMapPosition(LeftLowMap, new Vector3(getNowX, getNowY, 0));
        }

        PlayMapInfo.LeftTopMap = LeftTopMap;
        PlayMapInfo.RightTopMap = RightTopMap;
        PlayMapInfo.LeftLowMap = LeftLowMap;
        PlayMapInfo.RightLowMap = RightLowMap;
    }

    private void SetMapPosition(GameObject Object, Vector3 position)
    {
        if (!Object) return;
        Object.transform.position = position;
    }
}
