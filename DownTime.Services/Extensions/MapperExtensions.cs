﻿using AutoMapper;

namespace DownTime.Services.Extensions
{
    public static class MapperExtensions
    {
        public static T Map<T>(this object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
