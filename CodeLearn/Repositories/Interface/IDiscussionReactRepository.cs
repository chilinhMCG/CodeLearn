﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IDiscussionReactRepository
    {
        int CountDiscussionReactFromDiscussion(string discussionid);

    }
}
