﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using STProject.Data;
using STProject.Data.Models;
using System;

namespace STProject.Services.Communities
{
    public enum CommunitiesSorting
    {
        DateCreated = 0,
        Name = 1,
    }

    public class CommunityService : ICommunityService
    {
        private readonly STProjectContext data;

        public CommunityService(STProjectContext data)
        {
            this.data = data;
        }

        public async Task<List<Community>> GetPaginatedResult(List<Community> communities, int currentPage, int pageSize = 1)
        {
            return communities.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Community> Search(string searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return data.Communities.ToList();

            return data.Communities.Where(c => c.Name.Contains(searchTerm) ||
                                              c.Description.Contains(searchTerm) ||
                                              c.Category.Name.Contains(searchTerm)).ToList();
        }

        public int GetCount()
        {
            return this.data.Communities.Count();
        }

        public int Create(Community community)
        {
            this.data.Communities.Add(community);
            this.data.SaveChanges();

            return community.Id;
        }

        public Community Details(int? id)
        {
            if (id == null || data.Communities == null)
            {
                return null;
            }

            var community = data.Communities.FirstOrDefault(m => m.Id == id);
            if (community == null)
            {
                return null;
            }
            else
            {
                return community;
            }
        }
    }
}
