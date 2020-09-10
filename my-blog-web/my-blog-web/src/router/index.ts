import {createRouter, createWebHistory, RouteRecordRaw} from "vue-router";
import historyRoutes from './history'

const routes: Array<RouteRecordRaw> = [
    {
        path: "/",
        name: "",
        component: () =>
            import ('@/views/Home.vue'),
    },
    {
        path: "/history",
        name: "",
        component: () =>
            import ('@/views/history/index.vue'),
        children: historyRoutes
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

export default router;
