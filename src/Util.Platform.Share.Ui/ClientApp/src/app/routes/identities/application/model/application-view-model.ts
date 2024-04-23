import { ViewModel } from "util-angular";

/**
* 应用程序参数
*/
export class ApplicationViewModel extends ViewModel {
   /**
    * 应用程序编码
    */
    code;
   /**
    * 应用程序名称
    */
    name;
   /**
    * 启用
    */
    enabled;
   /**
    * 备注
    */
    remark;
   /**
    * 创建时间
    */
    creationTime;
   /**
    * 创建人标识
    */
    creatorId;
   /**
    * 最后修改时间
    */
    lastModificationTime;
   /**
    * 最后修改人标识
    */
    lastModifierId;
   /**
    * 版本号
    */
    version;
}
