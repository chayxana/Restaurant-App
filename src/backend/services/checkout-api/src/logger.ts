import pino from "pino";

function getLogger() {
    return pino({
        level: process.env.LOG_LEVEL || 'info',
        formatters: {
            level: (label) => {
                return { level: label.toUpperCase() };
            },
        },
    })
};

export const logger = getLogger()