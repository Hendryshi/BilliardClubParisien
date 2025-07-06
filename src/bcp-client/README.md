# 台球俱乐部会员管理系统 (Club de Billard - Système de Gestion des Membres)

## 项目简介
这是一个基于 Angular 开发的台球俱乐部会员管理系统，专门面向法国的台球俱乐部。系统提供会员在线申请和管理员审核功能，支持移动端和桌面端自适应访问。

## 主要功能

### 会员端功能
- 在线注册账号（主页功能）
- 表单验证和错误提示
- 图片上传功能（最多2张，5MB限制）
- 注册成功页面，支持返回注册页面或重新注册

### 管理员功能
- 管理员控制面板
- 会员申请审核
- 新会员验证
- 现有会员管理

## 技术规格
- 框架：Angular 17+
- UI组件：Angular Material
- 响应式设计：支持移动端和桌面端
- 本地化：法语界面
- 身份认证：JWT
- 支付集成：Stripe

## 开发环境要求
- Node.js (v18+)
- npm (v9+)
- Angular CLI (v17+)

## 项目结构

# API集成说明

本项目使用OpenAPI Generator自动生成API客户端代码，实现了与后端Swagger接口的无缝集成。

## 生成API客户端代码

1. 确保swagger.json文件位于项目根目录
2. 运行命令生成API代码：npm run generate-api
3. 修改 import { environment } from '../../../environments/environment';
    protected basePath: string = environment.apiUrl;

# 最近更新

## 2024年功能改进

### 注册成功页面优化
- 创建了独立的成功页面组件 (`/inscription/success`)
- 注册成功后自动跳转到专门的成功页面
- 成功页面包含详细的后续步骤说明
- 提供返回注册页面和重新注册两个操作选项
- 响应式设计，支持移动端和桌面端

### 用户体验改进
- 独立的成功页面提供更好的用户体验
- 清晰的成功反馈和后续操作指引
- 保持用户对操作结果的控制权
- 符合现代Web应用的最佳实践

