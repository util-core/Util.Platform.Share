import { Component,Injector } from '@angular/core';
import { environment } from 'environment';
import { ComponentBase } from 'util-angular';

/**
 * ģ�����
 */
export class ModuleViewModel {
    /**
     * Ӧ�ó����ʶ
     */
    applicationId;
    /**
     * Ӧ�ó�������
     */
    applicationName;
    /**
     * ģ������
     */
    name;
    /**
     * �����Լ���
     */
    i18n;
    /**
     * ģ���ַ
     */
    uri;
    /**
     * ͼ��
     */
    icon;
    /**
     * �Ƿ�����
     */
    isHide;
    /**
     * ��ע
     */
    remark;
    /**
     * ����ʱ��
     */
    creationTime;
}

@Component({
    selector: 'app-root',
    templateUrl: environment.production ? './index.html' : '/view/src/app/index',
    styleUrls: ['./app.component.less']
})
export class AppComponent extends ComponentBase{

    model: ModuleViewModel;

    /**
     * ѡ��ͼ��
     */
    selectedIcon(icon) {
        this.model.icon = icon;
    }

    constructor(injector: Injector) {
        super(injector);
        this.model = new ModuleViewModel();
    }
}
