using Microsoft.AspNetCore.Identity;

namespace Util.Platform.Share.Identity.Domain.Identity; 

/// <summary>
/// Identity本地化错误描述
/// </summary>
public class IdentityErrorLocalizationDescriber : IdentityErrorDescriber {
    /// <summary>
    /// 本地化查找器
    /// </summary>
    private readonly IStringLocalizer _localizer;

    /// <summary>
    /// 初始化Identity本地化错误描述
    /// </summary>
    /// <param name="localizerFactory">本地化查找器工厂</param>
    public IdentityErrorLocalizationDescriber( IStringLocalizerFactory localizerFactory ) {
        localizerFactory.CheckNull( nameof( localizerFactory ) );
        _localizer = localizerFactory.Create( "IdentityError", null );
    }

    /// <summary>
    /// 默认错误
    /// </summary>
    public override IdentityError DefaultError() {
        return new IdentityError {
            Code = nameof( DefaultError ),
            Description = _localizer[nameof( DefaultError )]
        };
    }

    /// <summary>
    /// 并发错误
    /// </summary>
    public override IdentityError ConcurrencyFailure() {
        return new IdentityError {
            Code = nameof( ConcurrencyFailure ),
            Description = _localizer[nameof( ConcurrencyFailure )]
        };
    }

    /// <summary>
    /// 密码错误
    /// </summary>
    public override IdentityError PasswordMismatch() {
        return new IdentityError {
            Code = nameof( PasswordMismatch ),
            Description = _localizer[nameof( PasswordMismatch )]
        };
    }

    /// <summary>
    /// 无效令牌
    /// </summary>
    public override IdentityError InvalidToken() {
        return new IdentityError {
            Code = nameof( InvalidToken ),
            Description = _localizer[nameof( InvalidToken )]
        };
    }

    /// <summary>
    /// 恢复代码补偿失败
    /// </summary>
    public override IdentityError RecoveryCodeRedemptionFailed() {
        return new IdentityError {
            Code = nameof( RecoveryCodeRedemptionFailed ),
            Description = _localizer[nameof( RecoveryCodeRedemptionFailed )]
        };
    }

    /// <summary>
    /// 用户已登录
    /// </summary>
    public override IdentityError LoginAlreadyAssociated() {
        return new IdentityError {
            Code = nameof( LoginAlreadyAssociated ),
            Description = _localizer[nameof( LoginAlreadyAssociated )]
        };
    }

    /// <summary>
    /// 无效用户名
    /// </summary>
    /// <param name="userName">用户名</param>
    public override IdentityError InvalidUserName( string userName ) {
        return new IdentityError {
            Code = nameof( InvalidUserName ),
            Description = string.Format( _localizer[nameof( InvalidUserName )], userName )
        };
    }

    /// <summary>
    /// 无效电子邮件
    /// </summary>
    /// <param name="email">电子邮件</param>
    public override IdentityError InvalidEmail( string email ) {
        return new IdentityError {
            Code = nameof( InvalidEmail ),
            Description = string.Format( _localizer[nameof( InvalidEmail )], email )
        };
    }

    /// <summary>
    /// 用户名重复
    /// </summary>
    /// <param name="userName">用户名</param>
    public override IdentityError DuplicateUserName( string userName ) {
        return new IdentityError {
            Code = nameof( DuplicateUserName ),
            Description = string.Format( _localizer[nameof( DuplicateUserName )], userName )
        };
    }

    /// <summary>
    /// 电子邮件重复
    /// </summary>
    /// <param name="email">电子邮件</param>
    public override IdentityError DuplicateEmail( string email ) {
        return new IdentityError {
            Code = nameof( DuplicateEmail ),
            Description = string.Format( _localizer[nameof( DuplicateEmail )], email )
        };
    }

    /// <summary>
    /// 角色名无效
    /// </summary>
    /// <param name="role">角色名</param>
    public override IdentityError InvalidRoleName( string role ) {
        return new IdentityError {
            Code = nameof( InvalidRoleName ),
            Description = string.Format( _localizer[nameof( InvalidRoleName )], role )
        };
    }

    /// <summary>
    /// 角色名重复
    /// </summary>
    /// <param name="role">角色名</param>
    public override IdentityError DuplicateRoleName( string role ) {
        return new IdentityError {
            Code = nameof( DuplicateRoleName ),
            Description = string.Format( _localizer[nameof( DuplicateRoleName )], role )
        };
    }

    /// <summary>
    /// 用户已设置密码
    /// </summary>
    public override IdentityError UserAlreadyHasPassword() {
        return new IdentityError {
            Code = nameof( UserAlreadyHasPassword ),
            Description = _localizer[nameof( UserAlreadyHasPassword )]
        };
    }

    /// <summary>
    /// 用户未启用锁定
    /// </summary>
    public override IdentityError UserLockoutNotEnabled() {
        return new IdentityError {
            Code = nameof( UserLockoutNotEnabled ),
            Description = _localizer[nameof( UserLockoutNotEnabled )]
        };
    }

    /// <summary>
    /// 用户在角色中已存在
    /// </summary>
    /// <param name="role">角色名</param>
    public override IdentityError UserAlreadyInRole( string role ) {
        return new IdentityError {
            Code = nameof( UserAlreadyInRole ),
            Description = string.Format( _localizer[nameof( UserAlreadyInRole )], role )
        };
    }

    /// <summary>
    /// 用户在角色中不存在
    /// </summary>
    /// <param name="role">角色名</param>
    public override IdentityError UserNotInRole( string role ) {
        return new IdentityError {
            Code = nameof( UserNotInRole ),
            Description = string.Format( _localizer[nameof( UserNotInRole )], role )
        };
    }

    /// <summary>
    /// 密码太短
    /// </summary>
    /// <param name="length">密码长度</param>
    public override IdentityError PasswordTooShort( int length ) {
        return new IdentityError {
            Code = nameof( PasswordTooShort ),
            Description = string.Format( _localizer[nameof( PasswordTooShort )], length )
        };
    }

    /// <summary>
    /// 密码需要包含不重复的字符数
    /// </summary>
    public override IdentityError PasswordRequiresUniqueChars( int uniqueChars ) {
        return new IdentityError {
            Code = nameof( PasswordRequiresUniqueChars ),
            Description = string.Format( _localizer[nameof( PasswordRequiresUniqueChars )], uniqueChars )
        };
    }

    /// <summary>
    /// 密码需要包含非字母和数字的特殊字符
    /// </summary>
    public override IdentityError PasswordRequiresNonAlphanumeric() {
        return new IdentityError {
            Code = nameof( PasswordRequiresNonAlphanumeric ),
            Description = _localizer[nameof( PasswordRequiresNonAlphanumeric )]
        };
    }

    /// <summary>
    /// 密码需要包含数字
    /// </summary>
    public override IdentityError PasswordRequiresDigit() {
        return new IdentityError {
            Code = nameof( PasswordRequiresDigit ),
            Description = _localizer[nameof( PasswordRequiresDigit )]
        };
    }

    /// <summary>
    /// 密码需要包含小写字母
    /// </summary>
    public override IdentityError PasswordRequiresLower() {
        return new IdentityError {
            Code = nameof( PasswordRequiresLower ),
            Description = _localizer[nameof( PasswordRequiresLower )]
        };
    }

    /// <summary>
    /// 密码需要包含大写字母
    /// </summary>
    public override IdentityError PasswordRequiresUpper() {
        return new IdentityError {
            Code = nameof( PasswordRequiresUpper ),
            Description = _localizer[nameof( PasswordRequiresUpper )]
        };
    }
}