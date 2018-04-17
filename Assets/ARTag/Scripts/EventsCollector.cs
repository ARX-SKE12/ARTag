﻿
namespace ARTag
{

    public class EventsCollector
    {
        public static readonly string RECONNECT = "connect";
        public static readonly string AUTH = "auth";
        public static readonly string AUTH_SUCCESS = "auth-success";
        public static readonly string AUTH_ERROR = "auth-error";
        public static readonly string PLACE_CREATE = "place-create";
        public static readonly string PLACE_CREATE_SUCCESS = "place-create-success";
        public static readonly string PLACE_CREATE_ERROR = "place-create-error";
        public static readonly string PLACE_RETRIEVE_SIGNIFICANT = "place-retrieve-significant";
        public static readonly string PLACE_RESPONSE_SIGNIFICANT = "place-response-significant";
        public static readonly string PLACE_ERROR_SIGNIFICANT = "place-error-significant";
        public static readonly string PLACE_LIST_REQUEST = "place-list-request";
        public static readonly string PLACE_LIST = "place-list";
        public static readonly string PLACE_LIST_ERROR = "place-list-error";
        public static readonly string PLACE_UPDATE = "place-update";
        public static readonly string PLACE_UPDATE_SUCCESS = "place-update-success";
        public static readonly string PLACE_UPDATE_ERROR = "place-update-error";
        public static readonly string PLANE_UPDATE = "plane-update";
        public static readonly string PLACE_CLEAR_PAGGING = "place-clear-pagging";
        public static readonly string PLACE_DATA_UPDATE = "place-data-update";
        public static readonly string ROOM_JOIN = "room-join";
        public static readonly string ROOM_LEAVE = "room-leave";
        public static readonly string ROOM_USER_ARRIVE = "room-arrive";
        public static readonly string ROOM_USER_LEFT = "room-left";
        public static readonly string ROOM_ERROR = "room-error";
        public static readonly string DISCONNECT = "disconnect";
    }

}
