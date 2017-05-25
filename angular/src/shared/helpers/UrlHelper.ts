/**
 * Url帮助类
 */
export class UrlHelper {
    /**
     * The URL requested, before initial routing.
     * 在初始路由之前，请求的URL。
     */
    static readonly initialUrl = location.href;

    /**
     * 获取Url的参数
     */
    static getQueryParameters(): any {
        return document.location.search.replace(/(^\?)/, '').split("&").map(function (n) { return n = n.split("="), this[n[0]] = n[1], this }.bind({}))[0];
    }
}