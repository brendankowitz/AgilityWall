using System;

namespace PortableTrello.Client.Fields
{
    [Flags]
    public enum MemberFields
    {
        all = 0,
        avatarHash                  = 1 << 0,
        bio                         = 1 << 1,
        bioData                     = 1 << 2,
        confirmed                   = 1 << 3,
        fullName                    = 1 << 4,
        idPremOrgsAdmin             = 1 << 5,
        initials                    = 1 << 6,
        memberType                  = 1 << 7,
        products                    = 1 << 8,
        status                      = 1 << 9,
        url                         = 1 << 10,
        username                    = 1 << 11,
        avatarSource                = 1 << 12,
        email                       = 1 << 13,
        gravatarHash                = 1 << 14,
        idBoards                    = 1 << 15,
        idBoardsPinned              = 1 << 16,
        idOrganizations             = 1 << 17,
        loginTypes                  = 1 << 18,
        newEmail                    = 1 << 19,
        oneTimeMessagesDismissed    = 1 << 20,
        prefs                       = 1 << 21,
        trophies                    = 1 << 22,
        uploadedAvatarHash          = 1 << 23,
        premiumFeatures             = 1 << 24,
    }
}